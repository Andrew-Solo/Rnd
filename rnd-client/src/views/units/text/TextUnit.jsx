import {TextField, Stack, Typography} from "@mui/material";

export default function TextUnit({editing, label, value}) {
  if (editing) return (
    <TextField label={label} value={value} helperText=" "/>
  )
  else return (
    <Stack spacing={1}>
      <Typography variant="caption" color="text.secondary">
        {label}
      </Typography>
      <Typography>
        {value}
      </Typography>
    </Stack>
  );
}