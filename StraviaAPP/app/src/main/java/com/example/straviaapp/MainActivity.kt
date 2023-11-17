package com.example.straviaapp

import android.Manifest.permission.ACCESS_FINE_LOCATION
import android.Manifest.permission.ACTIVITY_RECOGNITION
import android.annotation.SuppressLint
import android.icu.text.SimpleDateFormat
import android.location.Location
import android.os.Build
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Looper
import android.os.SystemClock
import android.util.Log
import android.view.View
import android.widget.Chronometer
import android.widget.TextView
import androidx.annotation.RequiresApi
import com.google.android.gms.location.*
import com.google.android.gms.maps.GoogleMap
import com.google.android.gms.maps.OnMapReadyCallback
import com.google.android.gms.maps.SupportMapFragment
import com.google.android.gms.maps.model.LatLng
import com.google.android.gms.maps.model.PolylineOptions
import com.vmadalin.easypermissions.EasyPermissions
import com.vmadalin.easypermissions.annotations.AfterPermissionGranted
import java.time.Instant
import java.time.format.DateTimeFormatter

/**
 * Actividad para rastrear usuario y tomar informacion de actividad
 */
class MainActivity : AppCompatActivity(), OnMapReadyCallback {

    private lateinit var mMap: GoogleMap
    private lateinit var lastLocation: Location
    private lateinit var timeStart: Instant
    private lateinit var distanceText: TextView
    private lateinit var chronometer: Chronometer
    private var locationList: MutableList<Location> = mutableListOf()
    private var distance = 0F
    lateinit var fusedLocationProviderClient: FusedLocationProviderClient
    var polylineOptions = PolylineOptions()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        distanceText = findViewById(R.id.tvKm)
        chronometer = findViewById(R.id.chronometerWidg)
        fusedLocationProviderClient = LocationServices.getFusedLocationProviderClient(this)
        createMapFragment()
    }

    private val locationCallback = object: LocationCallback() {
        override fun onLocationResult(locationResult: LocationResult?) {
            super.onLocationResult(locationResult)
            locationResult ?: return
            locationResult.locations.forEach {
                Log.d("TAG", "New location got: (${it.latitude}, ${it.longitude})")

                // si hay un lastLocation y se inicia la actividad, se agrega la distancia entre el punto
                // anterior y el actual a la distancia total recorrida.
                if(this@MainActivity::lastLocation.isInitialized){
                    distance += lastLocation.distanceTo(it)
                    distanceText.text = String.format("Distance: %.2f Km", distance / 1000)

                }
                locationList.add(it)
                lastLocation = it
                println(distance)
            }
            addLocationToRoute(locationResult.locations)


        }
    }

    companion object {
        // Permission
        private const val REQUEST_CODE_FINE_LOCATION = 1
        private const val REQUEST_CODE_ACTIVITY_RECOGNITION = 2
    }


    /**
     * Se dibuja ruta en mapa
     */
    fun addLocationToRoute(locations: List<Location>) {
        mMap.clear()
        val originalLatLngList = polylineOptions.points
        val latLngList = locations.map {
            LatLng(it.latitude, it.longitude)

        }
        originalLatLngList.addAll(latLngList)
        mMap.addPolyline(polylineOptions)
    }

    /**
     * Listener de ubicacion de usuario
     */
    @SuppressLint("MissingPermission")
    @AfterPermissionGranted(REQUEST_CODE_FINE_LOCATION)
    private fun setupLocationChangeListener() {
        if (EasyPermissions.hasPermissions(this, ACCESS_FINE_LOCATION)) {
            val locationRequest = LocationRequest()
            locationRequest.priority = LocationRequest.PRIORITY_HIGH_ACCURACY
            locationRequest.interval = 5000 // 5000ms (5s)
            fusedLocationProviderClient.requestLocationUpdates(locationRequest, locationCallback, Looper.getMainLooper())
        } else {
            // Do not have permissions, request them now
            EasyPermissions.requestPermissions(
                host = this,
                rationale = "For showing your current location on the map.",
                requestCode = REQUEST_CODE_FINE_LOCATION,
                perms = *arrayOf(ACCESS_FINE_LOCATION)
            )
        }
    }


    /**
     * Se crea mapa en pantalla
     */
    private fun createMapFragment() {
        val mapFragment = supportFragmentManager.findFragmentById(R.id.map) as SupportMapFragment
        mapFragment.getMapAsync(this)

    }


    override fun onMapReady(googleMap: GoogleMap) {
        mMap = googleMap
        showUserLocation()
    }

    /**
     * Muestra ubicacion de usuario
     */
    @SuppressLint("MissingPermission")
    @AfterPermissionGranted(REQUEST_CODE_FINE_LOCATION)
    private fun showUserLocation() {
        if (EasyPermissions.hasPermissions(this, ACCESS_FINE_LOCATION)) {
            mMap.isMyLocationEnabled = true
        } else {
            // Do not have permissions, request them now
            EasyPermissions.requestPermissions(
                host = this,
                rationale = "For showing your current location on the map.",
                requestCode = REQUEST_CODE_FINE_LOCATION,
                perms = *arrayOf(ACCESS_FINE_LOCATION)
            )
        }
    }

    /**
     * Llama metodos para iniciar la actividad
     */
    //@AfterPermissionGranted(REQUEST_CODE_ACTIVITY_RECOGNITION)
    @RequiresApi(Build.VERSION_CODES.O)
    fun startTracking(view: View) {
            setupLocationChangeListener()
            timeStart = Instant.now()
            chronometer.base = SystemClock.elapsedRealtime()
            chronometer.start()
    }

    /**
     * Detiene actividad y guarda datos para pasarlos a base de datos
     */
    @RequiresApi(Build.VERSION_CODES.O)
    fun stopActivity(view: View) {
        chronometer.stop()
        generateGPX("route", locationList)
    }

    /**
     * Genera archivo .gpx a partir de una lista de puntos de mapa
     */
    @RequiresApi(Build.VERSION_CODES.O)
    private fun generateGPX(name: String, points: List<Location>): String {
        val header = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\" ?><gpx xmlns=\"http://www.topografix.com/GPX/1/1\" creator=\"StraviaApp 0.1.0\" version=\"1.1\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd\"><trk>\n"
        val name = "<name>$name</name><trkseg>\n"
        var segments = ""
        val df = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ssZ")
        points.forEach{ location ->
            segments += "<trkpt lat=\"" + location.latitude +
                    "\" lon=\"" + location.longitude +
                    "\"><time>" +
                    DateTimeFormatter.ISO_INSTANT.format(
                        Instant.ofEpochMilli(location.time)
                    ) + "</time></trkpt>\n";
        }
        val footer = "</trkseg></trk></gpx>"
        return header + name + segments + footer
    }


}