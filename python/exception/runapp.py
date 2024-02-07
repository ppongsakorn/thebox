import logging
logging.basicConfig(level=logging.DEBUG,
format='%(asctime)s %(levelname)s %(message)s',
      filename='myapp.log',
      filemode='w')

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

    except Exception as e:
        print(e)
        #logging.debug(e)
        #logging.info(e)
        #logging.error(e)


print("resut : ",sum(20,10))