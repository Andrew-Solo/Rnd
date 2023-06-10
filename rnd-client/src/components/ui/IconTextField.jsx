import {InputAdornment, TextField} from "@mui/material";

export default function IconTextField({icon, ...props}) {
  return (
    <TextField InputProps={{startAdornment: (<InputAdornment position="start">{icon}</InputAdornment>)}} {...props}/>
  );
}