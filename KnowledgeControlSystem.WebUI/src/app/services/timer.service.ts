import { Pipe, PipeTransform } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

export class TimerService {

  transform(value: number): string {
    const minutes: number = Math.floor(value / 60);
    return ('00' + minutes).slice(-2) + ':' + ('00' + Math.floor(value - minutes * 60)).slice(-2);
  }

  startTimer(durationSeconds: number, display: HTMLElement) {
    let minutes;
    let seconds;
    let timer: number = durationSeconds;
    setInterval(function () {
      minutes = (timer / 60) | 0;
      seconds = (timer % 60) | 0;
      minutes = minutes < 10 ? "0" + minutes : minutes;
      seconds = seconds < 10 ? "0" + seconds : seconds;
      display.textContent = minutes + ":" + seconds;
      if (--timer < 0) {
        timer = durationSeconds;
      }
    }, 1000);
  }


}