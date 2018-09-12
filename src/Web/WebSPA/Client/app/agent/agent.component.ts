import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { AgentWrapperService } from './agent.wrapper.service';

@Component({
  selector: 'agent',
  styles: [],
  templateUrl: 'agent.component.html',
  providers: []
})

export class AgentComponent implements OnInit { 
    showHeaderMenu = false;

    constructor(private agnetEvents: AgentWrapperService) {

    }

    previousAgentClicked() {
        debugger;
        this.agnetEvents.previousAgentClicked();
    }

    nextAgentClicked() {
        debugger;
        this.agnetEvents.nextAgentClicked();
    }

    ngOnInit(): void {
        this.agnetEvents.headerMenuToggler$.subscribe(
            param => {
                this.showHeaderMenu = param;
            });
    }
}
