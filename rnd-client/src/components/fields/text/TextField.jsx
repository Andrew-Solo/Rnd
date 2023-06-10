import {TextField as MuiTextField, Stack, Typography} from "@mui/material";

export default function TextField({editing, label, value}) {
  if (editing) return (
    <MuiTextField label={label} value={value} helperText=" "/>
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