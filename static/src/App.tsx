import {
  CssBaseline,
  Grid
} from '@material-ui/core';
import {
  createTheme,
  ThemeProvider,
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  Typography
} from '@material-ui/core';
import { indigo, blue } from '@material-ui/core/colors';
import NavBar from './components/NavBar';
import SearchBar from './components/SearchBar';
import MediaGrid from './components/MediaGrid';
import Footer from './components/Footer';


const theme = createTheme({
  palette: {
    type: 'light',
    primary: indigo,
    secondary: blue
  }
});


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        updates: {
            marginLeft: '24px'
        },
        mediaGrid: {
            overflowY: 'auto'
        }
    }),
);


function App() {
  const classes = useStyles();

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline/>
      <Grid
        container
        direction="column"
        justifyContent="flex-start"
        alignItems="stretch"
      >
        <NavBar />
        <SearchBar />
        <Typography
          gutterBottom
          variant="h6"
          className={classes.updates}
        >
          Recent updates
        </Typography>
        <MediaGrid />
        <Footer />
      </Grid>
    </ThemeProvider>
  );
}

export default App;
