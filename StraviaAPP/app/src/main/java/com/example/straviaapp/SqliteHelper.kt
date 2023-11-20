package com.example.straviaapp

import android.content.ContentValues
import android.content.Context
import android.database.sqlite.SQLiteOpenHelper
import android.database.sqlite.SQLiteDatabase
import android.provider.BaseColumns

class SqliteHelper(context: Context) : SQLiteOpenHelper(context, DATEBASE_NAME, null, DATABASE_VERSION) {

    companion object{
        private const val DATABASE_VERSION = 1
        private const val DATEBASE_NAME = "GPX.db"
        private const val TBL_GPX = "tbl_gpx"
        private val FILE = "file"
    }
    object TableGPX : BaseColumns {
        const val TABLE_NAME = "TblGPX"
        const val COLUMN_NAME = "GPX"
    }
    private val SQL_CREATE_ENTRIES =
        "CREATE TABLE ${TableGPX.TABLE_NAME} (" +
                "${BaseColumns._ID} INTEGER PRIMARY KEY," +
                "${TableGPX.COLUMN_NAME} TEXT)"

    private val SQL_DELETE_ENTRIES = "DROP TABLE IF EXISTS ${TableGPX.TABLE_NAME}"
    override fun onCreate(db: SQLiteDatabase?) {
        db?.execSQL(SQL_CREATE_ENTRIES)
    }

    override fun onUpgrade(db: SQLiteDatabase?, oldVersion: Int, newVersion: Int) {
        db!!.execSQL(SQL_DELETE_ENTRIES)
        onCreate(db)
    }

    fun insertGPX(data: String){
        val db = writableDatabase
        val values = ContentValues().apply {
            put(TableGPX.COLUMN_NAME, data)
        }
        db.insert(TableGPX.TABLE_NAME, null, values)
    }
}
