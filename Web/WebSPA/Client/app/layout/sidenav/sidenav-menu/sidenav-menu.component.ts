import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Guid } from '../../../shared/models/guid';

@Component({
  selector: 'my-app-sidenav-menu',
  styles: [],
  templateUrl: './sidenav-menu.component.html'
})

export class AppSidenavMenuComponent implements OnInit {
  currentUrl: string;
  constructor(private router: Router) {
    router.events.subscribe((_: NavigationEnd) => this.currentUrl = _.url);
  }

  ngOnInit() {}
}