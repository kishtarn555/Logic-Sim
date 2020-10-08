using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Sim.Utils
{
    class Priority_queue<T> where T:IComparable {
        int size;
        element<T> head;
        public int Size {
            get { return size; }            
        }
        public T Top {
            get {
                if (head == null) {
                    throw new InvalidOperationException("Priority queue is empty");
                }
                return head.value; 
            }
        }
        public Priority_queue() {
            head = null;
            size = 0;
        }
        public bool Any() {
            return head != null;
        }
        public void Push(T newEl) {
            size++;
            element<T> next = new element<T>(newEl);
            if (head==null) {
                head = next;
                return;
            }
            element<T> cur = head;
            while (cur.left != null) cur = cur.left;
            cur.left = next;
            next.parent = cur;

            T temp;
            while (next.parent != null && next.value.CompareTo(next.parent.value)<0) {
                temp = cur.value;
                cur.value = cur.parent.value;
                cur.parent.value = temp;
                cur = cur.parent;
            }
        }
        public T Pop() {
            if (head==null) {
                throw new InvalidOperationException("Priority queue is empty");
            }
            size--;
            T ret = head.value;
            if (size == 0) {
                head = null;
                return ret;
            }
            element<T> cur = head, next;
            while (cur.left !=null) {
                cur = cur.left;
            }
            T temp = cur.value; ;
            cur.value = head.value;
            head.value = temp;
            cur.parent.left = null;
            cur = head;
            int flag;
            while (cur.left!=null|| cur.right!=null) {
                flag = 0;
                if (cur.left != null && cur.value.CompareTo(cur.left.value) > 0) flag |= 1;
                if (cur.right != null && cur.value.CompareTo(cur.right.value) > 0) flag |= 2;
                if (flag == 0) break;
                if (flag==1) {
                    next = cur.left;
                } else if (flag==2) {
                    next = cur.right;
                } else {                    
                    if (cur.left.value.CompareTo(cur.right.value)<0) {
                        next = cur.left;
                    } else {
                        next = cur.right;
                    }
                }
                temp = cur.value;
                cur.value = next.value;
                next.value = temp;
                cur = next;
            } 
            return ret;
        }

        private class element<T> {
            public T value;
            public element<T> right, left,parent;
            public element(T val) {
                value = val;
                right = left = parent=null;
            }
        }
    }
}
