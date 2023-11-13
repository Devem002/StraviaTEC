import { Component} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.css']
})
export class RegistrarUsuarioComponent {
  formularioRegistro: FormGroup;
  fotoPreview: string | ArrayBuffer | undefined;
  nacionalidades?: string[];

  // Constructor del componente
  constructor(private formBuilder: FormBuilder, private router: Router) {
    this.formularioRegistro = this.formBuilder.group({
      nombre: ['', Validators.required],
      apellido1: ['', Validators.required],
      apellido2: ['', Validators.required],
      fechaNacimiento: ['', Validators.required],
      edad: ['', [Validators.required, Validators.min(18)]],
      nacionalidad: ['', Validators.required],
      foto: ['', Validators.required],
      usuario: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
    
    // Josue esto es lo que hay que cambiar para meter nacionalidades
    this.nacionalidades = ['España', 'Francia', 'Alemania', 'Italia', 'Portugal']; 
  }


  // Función que se ejecuta al enviar el formulario
  registrarUsuario(): void {
    console.log('Formulario: ', this.formularioRegistro.value);
    this.router.navigate(['']);
  }

  // Función que se ejecuta al seleccionar una foto
  mostrarPrevisualizacion(event: Event): void { 
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.fotoPreview = reader.result as string | ArrayBuffer;
      };
      reader.readAsDataURL(file);
    } else {
      this.fotoPreview = undefined;
    }
  }
}