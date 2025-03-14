import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NavBar} from './navbar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavBar],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AptitudeTestClient';
}
