import {Avatar, Box, Typography} from "@mui/material";

export default function Account () {
  return (
    <Box height="80px" width="100%" display="flex" gap="8px" justifyContent="center" alignContent="center">
      <Typography variant="h4">
        AndrewSolo
      </Typography>
      <Avatar/>
    </Box>
  );
}