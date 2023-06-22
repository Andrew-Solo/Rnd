import TextUnit from "./text/TextUnit";

export default function Field({type, ...props}) {
  switch (type) {
    case "text": return(<TextUnit {...props}/>)
    default: return(<TextUnit {...props}/>)
  }
}