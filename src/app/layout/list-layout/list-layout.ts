import { Component } from '@angular/core';
import { Header } from '../../shared/components/header/header';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-list-layout',
  imports: [RouterOutlet, Header],
  templateUrl: './list-layout.html',
  styleUrl: './list-layout.css',
})
export class ListLayout {

}
