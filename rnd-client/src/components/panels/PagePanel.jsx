import {Box} from "@mui/material";
import PageHeader from "./PageHeader";

export default function PagePanel({children, ...props}) {
  return (
    <Box component="main" width={1} padding={4} gap={4} display="flex" flexDirection="column">
      <PageHeader {...props}/>
      {children}
    </Box>
  );
}