import Message from "./Message";

export default class Result<T> {
  constructor(message: Message | string, data: T | undefined | null = null) {
    this.data = data;
    this.message = typeof message === "string" ? new Message({title: message}) : message;
  }

  data: T | null
  message: Message
}