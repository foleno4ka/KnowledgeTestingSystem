import { Pipe, PipeTransform } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Pipe({
  name: 'formatTime'
})
export class TimerService implements PipeTransform {

  transform(value: number): string {
    const minutes: number = Math.floor(value / 60);
    return ('00' + minutes).slice(-2) + ':' + ('00' + Math.floor(value - minutes * 60)).slice(-2);
  }

}