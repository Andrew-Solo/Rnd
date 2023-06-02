import {Avatar, Box, Typography} from "@mui/material";

export default function Brand() {
  return (
    <Box height="80px" width="100%" display="flex" gap="12px" justifyContent="center" alignContent="center">
      <Avatar/>
      <Typography variant="brand" color="primary">
        Rock'n'Dice
      </Typography>
    </Box>
  );
}