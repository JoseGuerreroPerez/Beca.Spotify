import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap, catchError, throwError} from 'rxjs';
import { INewSong } from '../playlists/create-song/INewSong';
import { ISong } from './isong';
import { IPlaylist } from './iplaylist';

@Injectable({
  providedIn: 'root'
})
export class PlaylistsService {
  baseUrl = "http://localhost:5211"

  constructor(private http: HttpClient) {
  }

  getPlaylist(shopId: string): Observable<IPlaylist> {
    return this.http.get<IPlaylist>(this.baseUrl + "/api/Playlists/" + shopId).pipe(
      tap(data => console.log('Shop:', JSON.stringify(data))),
      catchError(this.handleError));
  }

  getPlaylists(): Observable<IPlaylist[]> {
    return this.http.get<IPlaylist[]>(this.baseUrl +"/api/Playlists").pipe(
      tap(data => console.log('Shops:', JSON.stringify(data))),
      catchError(this.handleError));
  }
  getSongs(shopId: string): Observable<ISong[]>  {
    return this.http.get<ISong[]>(this.baseUrl + "/api/playlist/" + shopId +"/Canciones").pipe(
      tap(data => console.log('Products:', JSON.stringify(data))),
      catchError(this.handleError));
  }
  getSong(shopId: string, productId: string): Observable<ISong> {
    return this.http.get<ISong>(this.baseUrl + "/api/playlist/" + shopId + "/Canciones/" + productId).pipe(
      tap(data => console.log('Product:', JSON.stringify(data))),
      catchError(this.handleError));
  }

  postSong(newProduct: INewSong, shopId: string): Observable<any> {
    console.log("Creando nuevo producto");
    return this.http.post(this.baseUrl + "/api/playlist/" + shopId + "/Canciones", newProduct).pipe(
      tap(data => console.log('Producto creado:', JSON.stringify(data))),
      catchError(this.handleError));
  }

  putSong(newProduct: INewSong, shopId: string, productId: string): Observable<any> {
    console.log("Editando producto");
    return this.http.put(this.baseUrl + "/api/playlist/" + shopId + "/Canciones/" + productId, newProduct).pipe(
      tap(_ => console.log('Producto modificado con éxito.')),
      catchError(this.handleError));
  }

  deleteSong(shopId: string, productId: number): Observable<any> {
    console.log("Eliminando un producto");
    return this.http.delete(this.baseUrl + "/api/playlist/" + shopId + "/Canciones/" + productId).pipe(
      tap(_ => console.log('Producto eliminado con éxito.')),
      catchError(this.handleError));
  }
  private handleError(err: HttpErrorResponse) {
    let errorMessage = err.error.message;
    console.error(errorMessage);
    return throwError(() => errorMessage)
  }

}
