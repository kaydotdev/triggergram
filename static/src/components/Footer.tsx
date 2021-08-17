import {
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  Typography,
  IconButton
} from '@material-ui/core';
import GithubLogo from '../graphics/github-logo.svg';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        appName: {
            fontWeight: 500,
            padding: '8px 0 0 12px',
            margin: 0
        },
        footer: {
            display: 'flex',
            justifyContent: 'space-between',
            margin: theme.spacing(3),
            width: 'calc(100% - 48px)',
            color: 'rgba(0, 0, 0, 0.87)',
            textAlign: 'center',
            backgroundColor: '#fafafa',
            borderColor: 'rgba(0, 0, 0, 0.30)',
            borderStyle: 'solid',
            borderRadius: '10px',
            borderWidth: '1px'
        }
    }),
);


function Footer() {
    const classes = useStyles();

    return (
      <footer className={classes.footer}>
          <Typography
          className={classes.appName}
          variant="overline"
          display="block"
          gutterBottom>
            {(new Date()).getFullYear()} (NC) TRIGGERGRAM
          </Typography>
          <IconButton
            aria-label="github project" onClick={() => {
              window.location.href = 'https://github.com/antonAce/triggergram';
            }}>
            <img src={GithubLogo} alt="TRIGGERGRAM" />
          </IconButton>
      </footer>
    );
}

export default Footer;
