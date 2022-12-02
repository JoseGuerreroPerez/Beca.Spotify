import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IPlaylist } from '../../shared/iplaylist';
import { PlaylistsService } from '../../shared/playlists.service';

@Component({
  selector: 'app-list-playlists',
  templateUrl: './list-playlists.component.html',
  styleUrls: ['./list-playlists.component.css']
})
export class ListShopsComponent implements OnInit, OnDestroy {

  constructor(private shopsService: PlaylistsService) { }
  elementsPerRow = 4;

  sub!: Subscription;
  shops: IPlaylist[] = [];
  rows: number[] = [0];

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  ngOnInit(): void {
    this.sub = this.shopsService.getPlaylists().subscribe({
      next: shops => {
        this.shops = shops;
        let nRows = Math.ceil(shops.length / this.elementsPerRow);
        this.rows = Array(nRows).fill(0).map((_, i) => i);
      }
    })
  }

}
