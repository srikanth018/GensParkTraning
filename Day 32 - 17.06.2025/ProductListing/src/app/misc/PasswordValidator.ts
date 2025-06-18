import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordValidator():ValidatorFn{
    return (control:AbstractControl):ValidationErrors|null => {
        const password = control.value as string;
        if(password?.length <=6 ) return {lengthError: "Password must contain atleast of 6 chars"}

        return null;
    }
}