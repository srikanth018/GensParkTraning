import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class customFromValidation {
  constructor() {}

  static UsernameValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const username = control.value as string;
      if (username == 'admin' || username == 'root')
        return { invalidUsername: 'Username should not have these words' };
      return null;
    };
  }

  static PasswordValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const pass = control.value as string;
      const haveNumbers = /[0-9]/.test(pass);
      const haveSymbols = /[!@#$%^&*(),.?":{}|<>]/.test(pass);
      if (!haveSymbols)
        return {
          passSymError: 'Password must contain at least one special character',
        };
      if (pass.length < 8)
        return { passLenError: 'Password must contain 8 letters' };
      if (!haveNumbers)
        return { passNumError: 'Password must contain at least one number' };
      return null;
    };
  }

  static ConfirmPassword(): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const password = group.get('password')?.value;
      const confirmPassword = group.get('confirmPassword')?.value;

      if (password && confirmPassword && password !== confirmPassword) {
        return { passwordMismatch: true };
      }
      return null;
    };
  }
}
