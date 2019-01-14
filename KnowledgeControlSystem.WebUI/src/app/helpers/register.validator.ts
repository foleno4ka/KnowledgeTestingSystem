import { Directive, forwardRef, Provider, Attribute } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl } from '@angular/forms';
//import { Validator, AbstractControl, NG_VALIDATORS }
@Directive({
  selector: `[validateEqual][formControlName],[validateEqual] 
  [formControl],[validateEqual][ngModel]`,
  providers: [
    {provide:NG_VALIDATORS, useExisting:forwardRef(() => EqualValidator), multi: true }
  ]
})
export class EqualValidator implements Validator {
  constructor( @Attribute('validateEqual') public validateEqual: string,
  @Attribute('reverse') public reverse: string) {}

  private get isReverse(){
    if (!this.reverse) return false;
    return this.reverse === 'true' ? true: false;
  }
  validate(c: AbstractControl): { [key: string]: any } {
    // self value (e.g. retype password)
    let v = c.value;
    // control value (e.g. password)
    let e = c.root.get(this.validateEqual);
    //value not equal
    if (e && v !== e.value && !this.isReverse) return {
      validateEqual: false
    }

    if (e && v === e.value && this.isReverse) {
        delete e.errors['validateEqual'];
        if (!Object.keys(e.errors).length) e.setErrors(null);
    }

    if (e && v !== e.value && this.isReverse) {
        e.setErrors({ validateEqual: false });
    }

      return null;
   }
}