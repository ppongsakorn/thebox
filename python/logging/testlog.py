import logging
logging.basicConfig(level=logging.DEBUG,
format='%(asctime)s %(levelname)s %(message)s',
      filename='myapp.log',
      filemode='a')

class Error(Exception):
    """Base class for other exceptions"""
    pass

class ValueTooSmallError(Error):
    """Raised when the input value is too small"""
    pass

def sum(a,b):
    try:
        if(a>b):
            raise ValueTooSmallError

        return a + b

    except ValueTooSmallError as e:
        print("This value tool large, try again.")
        logging.debug("This value tool large, try again.")
        logging.info("This value tool large, try again.")
        logging.error("This value tool large, try again.")

print("resut : ",sum(11,10))