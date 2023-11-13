import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrarUsuarioComponent } from './registrar-usuario/registrar-usuario.component';
import { IniciarSesionComponent } from './iniciar-sesion/iniciar-sesion.component';

const routes: Routes = [
  
  { path: 'iniciar-sesion', component: IniciarSesionComponent },
  { path: '', redirectTo: '/iniciar-sesion', pathMatch: 'full' },

  { path: 'registrar-usuario', component: RegistrarUsuarioComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
