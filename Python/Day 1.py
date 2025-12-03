# Read data
with open('day1input.txt', 'r', encoding='UTF=8') as f:
    lines = f.readlines()

dialNum = 50
zeroCounter = 0
for line in lines:
    direction = line[0]
    turnSize = int(line[1:])

    if direction == "L":
        dialNum -= turnSize
    if direction == "R":
        dialNum += turnSize

    if dialNum % 100 == 0:
        zeroCounter += 1

# Part One Answer
print(zeroCounter)

def calculateAdjustment(start : int, turn : int):
    startDial = start % 100
    rollOvers = abs((startDial + turn) // 100)
    endDial = (startDial + turn) % 100 
    if (turn < 0) and (startDial == 0):
        rollOvers = max(rollOvers - 1, 0)

    if (turn < 0) and (endDial == 0):
        adjustment = rollOvers + 1
    else:
        adjustment = rollOvers

    return adjustment, endDial

dialNum = 50
zeroCounter = 0
for line in lines:
    direction = line[0]
    turnSize = int(line[1:])

    startPosition = int(dialNum)
    if direction == "L":
        adjustment, dialNum  = calculateAdjustment(startPosition, -turnSize)
    if direction == "R":
        adjustment, dialNum = calculateAdjustment(startPosition, turnSize)

    zeroCounter += adjustment

# Part Two Answer
print(zeroCounter)