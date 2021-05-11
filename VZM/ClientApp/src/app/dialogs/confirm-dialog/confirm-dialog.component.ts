import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

@Component({
  templateUrl: 'confirm-dialog.component.html',
})
export class ConfirmDialogComponent {
  
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
