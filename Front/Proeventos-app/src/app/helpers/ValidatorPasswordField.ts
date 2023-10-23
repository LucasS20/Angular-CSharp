export class ValidatorPasswordField {

    static MustMatch(controlName: string, matchingControlName: string) {
        return controlName.match(matchingControlName);
    }
}
