import { Routes, RouterModule } from '@angular/router';

import { AgentComponent } from './agent.component';

//import { AgentFilterComponent } from './filter/filter.component';
import { AgentListViewComponent } from './listview/list.component';
import { AgentDetailViewComponent } from './detailview/detail.component';
import { AgnetAddComponent } from './add/addagent.component';

export const AgentRoutes: Routes = [
  {
    path: '',
    component: AgentComponent,
    children: [
      { path: '', redirectTo: '/app/dashboard', pathMatch: 'full' },
      //{ path: 'filter', component: AgentFilterComponent  },
      { path: 'list', component: AgentListViewComponent },
      { path: 'detail', component: AgentDetailViewComponent },
      { path: 'addnew', component: AgnetAddComponent },
    ]
  }
];

export const AgentRoutingModule = RouterModule.forChild(AgentRoutes);
