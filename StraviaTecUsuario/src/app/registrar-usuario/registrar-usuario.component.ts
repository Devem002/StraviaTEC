import { Component,Inject} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'; 



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
  constructor(private formBuilder: FormBuilder, private router: Router, private dialog: MatDialog) {
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