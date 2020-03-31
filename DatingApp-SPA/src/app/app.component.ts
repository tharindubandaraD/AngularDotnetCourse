import { Component } from '@angular/core';
// this is a decorator that gicves angular featuers
@Component({
  // this is the list of css and template files and all
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DatingApp-SPA-tharindu';
}
