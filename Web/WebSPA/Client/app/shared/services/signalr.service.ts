﻿import { Injectable } from '@angular/core';
import { SecurityService } from './security.service';
import { ConfigurationService } from './configuration.service';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { MatSnackBar } from '@angular/material';
import { Subject } from 'rxjs';

@Injectable()
export class SignalrService {

    private _hubConnection: HubConnection;
    private SignalrHubUrl: string = '';
    private msgSignalrSource = new Subject();
    msgReceived$ = this.msgSignalrSource.asObservable();

    constructor(
        private securityService: SecurityService,
        private configurationService: ConfigurationService, private snackBar: MatSnackBar,
    ) {
        if (this.configurationService.isReady) {
            this.SignalrHubUrl = this.configurationService.serverSettings.signalrHubUrl;
            this.init();
        }
        else {
            this.configurationService.settingsLoaded$.subscribe(x => {
                this.SignalrHubUrl = this.configurationService.serverSettings.signalrHubUrl;
                this.init();
            });
        }
    }

    public stop() {
        this._hubConnection.stop();
    }

    private init() {
        if (this.securityService.IsAuthorized == true) {
            this.register();
            this.stablishConnection();
            this.registerHandlers();
        }
    }

    private register() {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(this.SignalrHubUrl + '/hub/notificationhub')
            .configureLogging(signalR.LogLevel.Information)
            .build();
        //accessTokenFactory: () => this.securityService.GetToken()  must needed

    }

    private stablishConnection() {
        this._hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(() => {
                console.log('Error while establishing connection')
            });
    }

    private registerHandlers() {
        this._hubConnection.on('UpdatedOrderState', (msg) => {
            this.snackBar.open('Updated to status: ' + msg.status, 'Order Id: ' + msg.orderId, {
                duration: 2000,
            });

            this.msgSignalrSource.next();
        });
    }

}