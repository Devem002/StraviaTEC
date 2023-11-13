import { Component,Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'; 


@Component({
  selector: 'app-iniciar-sesion',
  templateUrl: './iniciar-sesion.component.html',
  styleUrls: ['./iniciar-sesion.component.css']
})
export class IniciarSesionComponent {
  formularioInicioSesion: FormGroup;

  // Constructor de la clase IniciarSesionComponent
  constructor(private formBuilder: FormBuilder, private router: Router, public dialog: MatDialog) {
    this.formularioInicioSesion = this.formBuilder.group({
      usuario: ['', Validators.required],
      contraseña: ['', Validators.required]
    });
  }

  // Función para iniciar sesión
  iniciarSesion() {
    let message: string;
    if (this.formularioInicioSesion.valid) {
      console.log(this.formularioInicioSesion.value);
      message = '¡Has iniciado sesión con éxito!'
    }
    else{
      message = '¡Ha habido un problema al iniciar sesión!';
    }
    this.dialog.open(DialogContentIniciar, {
      data: { message: message } ,
      width: 'fit-content', 
    });
  }

  // Función para registrar usuario
  registrarUsuario(): void {
    this.router.navigate(['/registrar-usuario']);
  }

}


// Alerta de inicio de sesión exitoso
@Component({
  selector: 'dialog-content-iniciar',
  template: `
    <h2 mat-dialog-title>Inicio de sesión</h2>
    <mat-dialog-content>{{ data.message }}</mat-dialog-content>
`,
  styleUrls: ['./iniciar-sesion.component.css']
})
export class DialogContentIniciar {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public dialogRef: MatDialogRef<DialogContentIniciar> 
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