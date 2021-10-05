import { AfterViewInit } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoggingService } from './logging/logging.service';
import { StuffService } from './stuff/stuff.service';

@Component({
  selector: 'valant-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent implements OnInit, AfterViewInit {
  public title = 'Valant demo';
  public data: string[];
  public mazeNames: string[];
  public mazeName: string;
  public mazeTemplate: any[];
  private canvas: HTMLCanvasElement;
  private canvasContext2D: CanvasRenderingContext2D;

  constructor(private logger: LoggingService, private stuffService: StuffService) {}

  ngOnInit() {
    this.logger.log('Welcome to the AppComponent');
    this.getStuff();
    this.getMazeNames();
  }

  ngAfterViewInit() {
    this.canvas = <HTMLCanvasElement>document.getElementById("canvasMaze");
    this.canvasContext2D = this.canvas.getContext("2d");
  }

  public uploadMazeTemplate(event): void {
    const file: File = event.target.files[0];
    this.stuffService.upload(file).subscribe({
      next: () => {
        this.logger.log('upload successful');
        this.getMazeNames();
      },
      error: (error) => {
        this.logger.error('Error uploading maze template: ', error);
      },
    });
  }

  private getMazeNames(): void {
    this.stuffService.getMazeNames().subscribe({
      next: (response: string[]) => {
        this.mazeNames = response;
      },
      error: (error) => {
        this.logger.error('Error getting maze names: ', error);
      },
    });
  }

  public getMazeTemplate(event): void {
    this.stuffService.getMazeTemplate(event.target.value).subscribe({
      next: (response: any) => {
        this.mazeTemplate = response;
        this.canvasContext2D.clearRect(0, 0, this.canvas.width, this.canvas.height);
        var height = this.mazeTemplate.length;
        var width = this.mazeTemplate[0].length;

        for (var i = 0; i < height; i++) {
          for (var j = 0; j < width; j++) {
            var val = this.mazeTemplate[i][j];
            this.canvasContext2D.font = "10px Arial";
            this.canvasContext2D.fillStyle = "fuchsia";
            this.canvasContext2D.fillText(val, (j+1) * 10, (i+1) * 30);
          }
        }
      },
      error: (error) => {
        this.logger.error('Error getting maze names: ', error);
      },
    });
  }

  private getStuff(): void {
    this.stuffService.getStuff().subscribe({
      next: (response: string[]) => {
        this.data = response;
      },
      error: (error) => {
        this.logger.error('Error getting stuff: ', error);
      },
    });
  }
}
