import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IPlaylist } from '../../shared/iplaylist';
import { PlaylistsService } from '../../shared/playlists.service';
import { MessagesService } from '../../shared/messages.service';
import { INewSong } from './INewSong';
import { ISong } from '../../shared/isong';

@Component({
  selector: 'app-create-song',
  templateUrl: './create-song.component.html',
  styleUrls: ['./create-song.component.css']
})
export class CreateProductComponent implements OnInit {


  templateNewProduct: INewSong = {
    name: undefined,
    description: undefined
  }
  newProduct: INewSong = { ...this.templateNewProduct }

  shopId?: string | null;
  shop?: IPlaylist;

  // Edición
  productToEditId?: string | null;

  constructor(private route: ActivatedRoute, private shopsService: PlaylistsService, private router: Router, private messages : MessagesService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => this.shopId = params.get('id'));
    if (this.shopId != null) {
      this.shopsService.getPlaylist(this.shopId).subscribe({
        next: shop => {
          this.shop = shop;
        }
      });
    }

    // Edición
    this.route.paramMap.subscribe(params => this.productToEditId = params.get('idProduct'));
    if (this.shopId != null && this.productToEditId != null) {
      this.shopsService.getSong(this.shopId, this.productToEditId).subscribe({
        next: product => {
        this.newProduct = product;
        } 
      });
    }
  }

  postError = false;
  postErrorMessage?: string;

  onSubmit(form: NgForm) {
    // Edita el producto
    if (form.valid && this.shopId != null && this.productToEditId != null) {
      this.shopsService.putSong(this.newProduct, this.shopId, this.productToEditId).subscribe(
        result => this.onHttpSuccess(result, "Producto editado con éxito"),
        error => this.onHttpError(error)
      )
    }
    // Crea un nuevo producto
    else if (form.valid && this.shopId != null) {
      this.shopsService.postSong(this.newProduct, this.shopId).subscribe(
        result => this.onHttpSuccess(result, "Producto creado con éxito"),
        error => this.onHttpError(error)
      )
    }
  }


  onHttpError(errorResponse: any) {
    console.error('error: ', errorResponse);
    this.postError = true;
    this.postErrorMessage = errorResponse.message;
  }

  onHttpSuccess(result: any, message: string) {
    console.log('success: ', result);
    if (this.shopId != null) { 
      this.messages.successMessage = message;
      this.router.navigate(['/shops/' + this.shopId]);
    }

  }

  onCancel() {
    this.router.navigate(['/shops/' + this.shopId]);

  }
}
