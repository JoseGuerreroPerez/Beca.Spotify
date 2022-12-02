import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListShopsComponent } from './list-playlists/list-playlists.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ListProductsComponent } from './list-songs/list-songs.component';
import { CreateProductComponent } from './create-song/create-song.component';



@NgModule({
  declarations: [
    ListShopsComponent,
    ListProductsComponent,
    CreateProductComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: 'shops/:id/new', component: CreateProductComponent },
      { path: 'shops/:id/edit/:idProduct', component: CreateProductComponent },
      { path: 'shops/:id', component: ListProductsComponent },
      { path: '', component: ListShopsComponent }
    ])
  ]
})
export class ShopsModule {
}
