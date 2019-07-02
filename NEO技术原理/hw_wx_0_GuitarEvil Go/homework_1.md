problem 1:base58check decoding
            
            
            去掉后四个字节校验位
                    |
                    V
        将每位字符按编码表转换成58进制数
                    |
                    V
           将58进制数转换为10进制数
                    |
                    V
          将10进制数转换为256进制数
                    |
                    V
            根据ascii码表转换为字符
            


problem 2: 
import hashlib


def sha256(data):
    return hashlib.new('sha256', data).digest()


def dsha256(data):
    return sha256(sha256(data))


def dhash256(left, right):
    return dsha256(left + right)


def calc_merkle_root(target_list):
    if len(target_list) == 1:
        return bytes.hex(target_list[0][::-1])
    temp= []
    #两次sha256
    for i in range(0, len(target_list)-1, 2):
        temp.append(dhash256(target_list[i], target_list[i+1]))
    #奇数
    if len(target_list) % 2 == 1:
        temp.append(dhash256(target_list[-1], target_list[-1]))
    return calc_merkle_root(temp)


if __name__ == '__main__':
    target_list = ['0000', '1111', '2222']
    bytes_target_list = [bytes.fromhex(target) for target in target_list]
    result = calc_merkle_root(bytes_target_list)
    print('Result is ' + result)





problem 3:UTXO
![image](https://github.com/GuitarevilGo/neo_training_course/blob/master/NEO%E6%8A%80%E6%9C%AF%E5%8E%9F%E7%90%86/hw_wx_0_GuitarEvil%20Go/UTXO.PNG)
