import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageServiceService {

  constructor() { }

  save(data: any){
    sessionStorage.setItem('asd',data);
  }
  get(id: any){
    return sessionStorage.getItem(id);
  }
  delete(id: any){
    sessionStorage.removeItem(id);
  }
  clearAll(){
    sessionStorage.clear();
  }
}