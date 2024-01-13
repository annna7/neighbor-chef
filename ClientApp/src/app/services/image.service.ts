import { Injectable } from '@angular/core';
import {AngularFireStorage} from "@angular/fire/compat/storage";
import {EMPTY, finalize, last, Observable, of, switchMap} from "rxjs";
import * as url from "url";


export enum ImageType {
  User = 'users',
  Meals = 'meals',
};

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private storage: AngularFireStorage) { }

  uploadImage(image: File, type: ImageType): Observable<string> {
    if (image === undefined) {
      return of('');
    }
    const filePath = `images/${type}/${image.name}_${new Date().getTime()}`;
    const fileRef = this.storage.ref(filePath);
    const task = this.storage.upload(filePath, image);
    return task.snapshotChanges().pipe(
      last(),
      switchMap(() => fileRef.getDownloadURL())
    );
  }
}
