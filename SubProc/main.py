#!python3
import time

def main():
    #print("input:")
    val = input("input:")
    print("value=", val)
    for i in range(12):
        print("Hello, World!")
        time.sleep(1)

    print("Exiting...")

    # raise Exception("Throwing Exception...")

if str(__name__).upper() in ("__MAIN__",):
    main()
