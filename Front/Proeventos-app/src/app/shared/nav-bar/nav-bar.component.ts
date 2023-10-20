import {Component} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  isCollapsed = true;
  protected readonly confirm = confirm;

  constructor(private router: Router) {
  }

  showNavBar(): boolean {
    return this.router.url !== '/user/login'
  }


}
