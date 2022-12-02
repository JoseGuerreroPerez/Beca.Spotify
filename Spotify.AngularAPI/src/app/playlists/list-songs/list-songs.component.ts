import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ISong } from '../../shared/isong';
import { IPlaylist } from '../../shared/iplaylist';
import { MessagesService } from '../../shared/messages.service';
import { PlaylistsService } from '../../shared/playlists.service';

@Component({
  selector: 'app-list-songs',
  templateUrl: './list-songs.component.html',
  styleUrls: ['./list-songs.component.css']
})
export class ListProductsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private shopsService: PlaylistsService, private messages: MessagesService) { }

  shopId?: string | null;
  allProducts: ISong[] = [];
  products: ISong[] = [];
  shop?: IPlaylist;
  successMessage?: string;
  errorMessage?: string;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => this.shopId = params.get('id'));
    this.loadProducts();
    this.loadShopDetails();

    if (this.messages.successMessage != undefined) {
      this.successMessage = this.messages.successMessage;
      this.messages.successMessage = undefined;
    }
  }

  loadProducts(): void {
    if (this.shopId != null) {
      this.shopsService.getSongs(this.shopId).subscribe({
        next: products => {
          this.allProducts = products;
          this.products = this.allProducts;
        }
      });
    }
  }

  loadShopDetails(): void {
    if (this.shopId != null) {
      this.shopsService.getPlaylist(this.shopId).subscribe({
        next: shop => {
          this.shop = shop;
        }
      });
    }
  }

  
  // Botón para volver a la lista de tiendas
  returnToShopList(): void {
    this.router.navigate(['/']);
  }

  // Boton para añadir producto
  newProduct(): void {
    if (this.shop != null) {
      this.router.navigate(['/shops/'+this.shop.id+"/new"])
    }
  }

  // Botón para editar producto
  editProduct(product: ISong) {
    if (this.shopId != null) {
      this.router.navigate(['/shops/' + this.shopId + '/edit/' + product.id]);
    }
  }

  // Botón para eliminar producto
  confirmDeletion(product: ISong) {
    Swal.fire({
      title: 'Confirmar eliminación',
      text: "El producto '" + product.name + "' será eliminado para siempre.",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    }).then((response: any) => {
      if (response.value && this.shopId != null) {
        this.shopsService.deleteSong(this.shopId, product.id).subscribe(
          result => this.onHttpSuccess(result),
          error => this.onHttpError(error)
        );
      } else if (response.dismiss === Swal.DismissReason.cancel) {
      }
    })
  }

  onHttpError(errorResponse: any) {
    console.error('error: ', errorResponse);
    this.errorMessage = "Ha ocurrido un error";
    this.successMessage = undefined;
  }

  onHttpSuccess(result: any) {
    console.log('success: ', result);
    if (this.shopId != null) {
      this.successMessage = "Producto eliminado con éxito";
      this.errorMessage = undefined;
      this.loadProducts();
    }
  }

  // Búsqueda por filtrado
  private _listFilter: string = "";

  get listFilter(): string {
    return this._listFilter;
  }

  set listFilter(value: string) {
    this._listFilter = value;
    this.products = this.performFilter(value);
  }

  performFilter(query: string): ISong[] {
    query = query.toLowerCase();
    return this.allProducts.filter((product: ISong) =>
      product.name.toLowerCase().includes(query));
  }

}
