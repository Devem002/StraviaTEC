import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tarjeta-usuario',
  templateUrl: './tarjeta-usuario.component.html',
  styleUrls: ['./tarjeta-usuario.component.css']
})
export class TarjetaUsuarioComponent implements OnInit {
  usuario = '';
  foto = '../../assets/StraviaTecLogo.png';
  siguiendo = 123;
  seguidores = 456;
  actividades = 789;
  ultimaActividad = 'Some text here';

  constructor(private sharedService: SharedService, private router: Router) { }

  // Función para inicializar el componente
  ngOnInit(): void {
    this.sharedService.usuarioActual.subscribe(usuario => this.usuario = usuario);
  }
  
  // Función para cerrar la sesión del usuario
  cerrarSesion(): void {
    this.usuario = '';
    this.sharedService.changeUsuario(this.usuario);
    this.router.navigate(['/iniciar-sesion']);
  }

}
