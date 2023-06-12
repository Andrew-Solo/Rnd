export default class Message {
  constructor(data: {title: string, [key:string]: any}) {
    this.title = data.title;
    this.details = data.details ?? new Set();
    this.tooltips = data.tooltips ?? {};
  }

  title: string
  details: Set<string>
  tooltips: {[name:string]: string}
}