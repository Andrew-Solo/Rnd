import {makeAutoObservable} from "mobx";
import React from "react";

export default class Form {
  constructor() {
    makeAutoObservable(this, {}, { autoBind: true });
  }

  handleChange(e: React.ChangeEvent<HTMLInputElement>): void {
    this.values[e.target.name] = e.target.value;
  }

  handleBlur(e: React.FocusEvent<HTMLInputElement>) {
    this.touched[e.target.name] = true;
  }

  createInputProps(name: string): any {
    return {
      name,
      error: this.touched[name] && this.errors[name],
      helperText: this.errors[name],
      value: this.values[name],
      onChange: this.handleChange,
      onBlur: this.handleBlur,
    }
  }

  values: {[key: string]: string} = {}
  errors: {[key: string]: string} = {}
  touched: {[key: string]: boolean} = {}
}