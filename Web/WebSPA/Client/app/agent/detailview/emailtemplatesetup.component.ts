import {Component, OnInit, ViewChild} from '@angular/core';
import {DataSource} from '@angular/cdk/collections';
import {MatPaginator, MatSort} from '@angular/material';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {merge, Observable, of as observableOf} from 'rxjs';
import {catchError, map, startWith, switchMap} from 'rxjs/operators';

import { IAgent } from '../agent.model';
import { AgentService } from '../agent.service';

/**
 * @title Table with pagination
 */
@Component({
    selector: 'agent-emailtemplate-setup',
    styleUrls: [],
    templateUrl: 'emailtemplatesetup.component.html',
})
export class AgnetEmailTemplateSetupComponent implements OnInit {

  

  constructor(private agentService: AgentService) {
     
  }

  ngOnInit() {
    
  }
}
