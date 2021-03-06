import {Component, OnInit, ViewChild} from '@angular/core';
import {DataSource} from '@angular/cdk/collections';
import {MatPaginator, MatSort} from '@angular/material';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {merge, Observable, of as observableOf} from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IAgent } from '../agent.model';
import { AgentService } from '../agent.service';
import { AgentWrapperService } from '../agent.wrapper.service';

/**
 * @title Table with pagination
 */
@Component({
  selector: 'agent-typeform-setup',
  styleUrls: [],
    templateUrl: 'typeformsetup.component.html',
})
export class AgnetTypeformSetupComponent implements OnInit {

    currentAgent = <IAgent>{};
    integrationEmail;
    isAgentProcessing: boolean;
    errorReceived: boolean;

    constructor(private agentService: AgentService,
        private agnetEvents: AgentWrapperService,
        private formBuilder: FormBuilder) {

    }

    generateTypeformClicked() {
        this.agentService.generateTypeformAccount(this.currentAgent.id)
            .catch((errMessage) => {
                this.errorReceived = true;
                this.isAgentProcessing = false;
                return Observable.throw(errMessage);
            })
            .subscribe(response => {
                debugger;
                this.currentAgent.agentTypeForm = response;
            });
    }

    ngOnInit() {
        this.agnetEvents.currentAgentChanged$.subscribe(
            agent => {
                this.currentAgent = agent;
            });
    } 
}
