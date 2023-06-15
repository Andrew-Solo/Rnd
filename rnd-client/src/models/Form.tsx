import {makeAutoObservable} from "mobx";
import {ChangeEvent, FormEvent, FocusEvent} from "react";

export default class Form {
  constructor(data: {}) {
    makeAutoObservable(this, {}, { autoBind: true });
  }

  handleSubmit(e: FormEvent<HTMLInputElement>): void {

  }

  handleChange(e: ChangeEvent<HTMLInputElement>): void {
    this.values[e.target.name] = e.target.value;
  }

  handleBlur(e: FocusEvent<HTMLInputElement>) {
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