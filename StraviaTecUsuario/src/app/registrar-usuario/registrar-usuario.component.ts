import { Component,Inject, OnInit} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'; 
import { ApiService } from '../api-service.service';
import { ThisReceiver } from '@angular/compiler';
import { BinImageService } from '../bin-image.service';



@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.css']
})
export class RegistrarUsuarioComponent implements OnInit {
  //Aqui hay que crear una interfaz para enviarla
  prueba:any;//Donde se mete la interfaz
  pruebaPaises:any;
  formularioRegistro: FormGroup;
  fotoPreview: string | ArrayBuffer | undefined;
  nacionalidades?: string[];

  // Constructor del componente
  constructor(private formBuilder: FormBuilder, private router: Router, private dialog: MatDialog, private apiService:ApiService, private bin_img:BinImageService) {
    this.formularioRegistro = this.formBuilder.group({
      nombre: ['', Validators.required],
      apellido1: ['', Validators.required],
      apellido2: ['', Validators.required],
      fechaNacimiento: ['', Validators.required],
      nacionalidad: ['', Validators.required],
      foto: ['', Validators.required],
      usuario: ['', Validators.required],
      password: ['', [Validators.required]]
    });
    
    // Josue esto es lo que hay que cambiar para meter nacionalidades
    this.nacionalidades = ['España', 'Francia', 'Alemania', 'Italia', 'Portugal']; 
    //this.nacionalidades = this.apiService.getDataTest();
  }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  // Función que se ejecuta al enviar el formulario
  registrarUsuario(): void {
    let message: string;
    if (this.formularioRegistro.valid) {
      console.log('Formulario: ', this.formularioRegistro.value);
      message = '¡Registro exitoso!';
      this.router.navigate(['']);
    } else {
      message = '¡Ha habido un problema al registrar!';
      this.formularioRegistro.reset();
    }
    console.log('Formulario: ', this.formularioRegistro.value);
    const dialogRef = this.dialog.open(DialogContentRegistrar, {
      data: { message: message },
      width: 'fit-content',
      panelClass: 'centered-dialog'
    });
  }

  // Función que se ejecuta al seleccionar una foto
  mostrarPrevisualizacion(event: Event): void {//Preguntar por el archivo para el convertidor en bits y guardarlo
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

  PostU(){//Llama el servicio para guardar el usuario
    this.apiService.postDataTest(this.prueba).subscribe(data => {
      console.log(this.prueba)
      console.log('Funca U')
    })
  }
}
// Alerta de inicio de sesión exitoso
@Component({
  selector: 'dialog-content-registrar',
  template: `
    <h2 mat-dialog-title>Registro</h2>
    <mat-dialog-content>{{ data.message }}</mat-dialog-content>
  `,
styleUrls: ['./registrar-usuario.component.css']
})
export class DialogContentRegistrar {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public dialogRef: MatDialogRef<DialogContentRegistrar> 
  ) {

    // Cierre automático de la alerta
    setTimeout(() => {
      this.dialogRef.close();
    }, 2000);
  }
}

interface DialogData {
  message: string;
}