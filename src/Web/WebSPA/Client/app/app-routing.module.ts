import { RouterModule, Routes } from '@angular/router';

import { LayoutComponent } from './layout/layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthenticationGuard } from './shared/services/authentication.guard';


const AppRoutes: Routes = [
  { path: '', redirectTo: '/app', pathMatch: 'full'  },
    { path: 'app', component: LayoutComponent },
    { path: '**', redirectTo: '/app/dashboard', pathMatch: 'full' },
];

export const AppRoutingModule = RouterModule.forRoot(AppRoutes, { useHash: true });
