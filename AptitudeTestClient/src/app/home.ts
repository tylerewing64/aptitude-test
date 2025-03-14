import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavBar} from './navbar';

@Component({
  selector: 'app-home',
  template: `<h3>Your are at the home page. Please use the navbar above to navigate to the Quotes page.</h3>`
})
export class Home {}
