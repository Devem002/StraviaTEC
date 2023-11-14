import { Injectable } from '@angular/core';
import * as fs from 'fs';

@Injectable({
  providedIn: 'root'
})

export class BinImageService {

  constructor() { }

  imageToBinary(imagePath: string): string {
    // Lee el archivo de imagen como un buffer
    const imageBuffer = fs.readFileSync(imagePath); 
    // Convierte el buffer a una cadena binaria
    const binaryImage = imageBuffer.toString('binary'); 
    return binaryImage;
  }

  binaryToImage(binaryString: string, outputPath: string): void {
    // Crea un buffer a partir de la cadena binaria
    const imageBuffer = Buffer.from(binaryString, 'binary');
    // Escribe el buffer en un archivo
    fs.writeFileSync(outputPath, imageBuffer);
  }
}
