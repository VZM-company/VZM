import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";

@Component({
  templateUrl: 'alert-dialog.component.html',
})
export class AlertDialogComponent {
  title: string;
  description: string;
  constructor(
    public dialogRef: MatDialogRef<AlertDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) {
    this.title = data['title'];
    this.description = data['description'];
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
