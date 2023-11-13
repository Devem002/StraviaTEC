import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatDialogModule } from '@angular/material/dialog';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ReactiveFormsModule } from '@angular/forms';
import { DialogContentRegistrar,RegistrarUsuarioComponent } from './registrar-usuario/registrar-usuario.component';
import { DialogContentIniciar, IniciarSesionComponent } from './iniciar-sesion/iniciar-sesion.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrarUsuarioComponent,
    IniciarSesionComponent,
    DialogContentIniciar,
    DialogContentRegistrar,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatDialogModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
