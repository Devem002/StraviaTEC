import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

// Clase para compartir datos entre componentes
// Si se necesita más variables simplemente hay que añadirlas aquí 
//y añadir las funciones para cambiarlas

// Añadir el import a la clase que se necesite y en el constructor private sharedService: SharedService
// Para cambiar el valor de la variable se usa this.sharedService.changeUsuario(usuario);
// Y para obtener el valor de la variable se usa this.sharedService.usuarioActual.subscribe(usuario => this.VariableNuevaDefininaEnElComponenteDondeSeVaAUsar = usuario);
export class SharedService {
  private usuarioSource = new BehaviorSubject<string>('');


  usuarioActual = this.usuarioSource.asObservable();


  constructor() { }

  changeUsuario(usuario: string) {
    this.usuarioSource.next(usuario);
  }

}