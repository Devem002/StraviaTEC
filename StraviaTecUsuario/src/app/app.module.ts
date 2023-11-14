import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatDialogModule } from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ReactiveFormsModule } from '@angular/forms';
import { DialogContentRegistrar,RegistrarUsuarioComponent } from './registrar-usuario/registrar-usuario.component';
import { DialogContentIniciar, IniciarSesionComponent } from './iniciar-sesion/iniciar-sesion.component';
import { TarjetaUsuarioComponent } from './tarjeta-usuario/tarjeta-usuario.component';
import { PaginaPrincipalComponent } from './pagina-principal/pagina-principal.component';
import { BarraNavegacionComponent } from './barra-navegacion/barra-navegacion.component';
import { PiePaginaComponent } from './pie-pagina/pie-pagina.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrarUsuarioComponent,
    IniciarSesionComponent,
    DialogContentIniciar,
    DialogContentRegistrar,
    TarjetaUsuarioComponent,
    PaginaPrincipalComponent,
    BarraNavegacionComponent,
    PiePaginaComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    MatDialogModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
