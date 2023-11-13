import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-iniciar-sesion',
  templateUrl: './iniciar-sesion.component.html',
  styleUrls: ['./iniciar-sesion.component.css']
})
export class IniciarSesionComponent {
  formularioInicioSesion: FormGroup;

  // Constructor de la clase IniciarSesionComponent
  constructor(private formBuilder: FormBuilder, private router: Router) {
    this.formularioInicioSesion = this.formBuilder.group({
      usuario: ['', Validators.required],
      contraseña: ['', Validators.required]
    });
  }

  // Función para iniciar sesión
  iniciarSesion() {
    if (this.formularioInicioSesion.valid) {
      console.log(this.formularioInicioSesion.value);
    }
  }

  // Función para registrar usuario
  registrarUsuario(): void {
    this.router.navigate(['/registrar-usuario']);
  }

}
